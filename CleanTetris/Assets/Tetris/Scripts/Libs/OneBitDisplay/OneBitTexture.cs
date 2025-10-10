using System;
using System.Collections.Generic;
using UnityEngine;

namespace Libs.OneBitDisplay
{
    public sealed class OneBitTexture : IDisposable
    {
        public enum StartCorner
        {
            BottomLeft,
            TopLeft
        }
        public int Width  { get; private set; }
        public int Height { get; private set; }
        public bool IsInitialized { get; private set; }
        public Texture2D Texture => _tex;

        private readonly HashSet<int> _lastActive = new();
        private readonly HashSet<int> _nextActive = new();
        private readonly Color32 _onColor = Color.white;
        private readonly Color32 _offColor = Color.clear;
        private Texture2D _tex;
        private byte[] _alpha;
        private StartCorner _startCorner;

        public void Initialize(
            int width, 
            int height, 
            StartCorner startCorner,
            FilterMode filter = FilterMode.Point, 
            TextureWrapMode wrap = TextureWrapMode.Clamp)
        {
            if (IsInitialized) 
                throw new InvalidOperationException("Already initialized");
            
            if (width <= 0 || height <= 0) 
                throw new ArgumentOutOfRangeException();

            Width = width;
            Height = height;
            _startCorner = startCorner;

            _alpha = new byte[width * height];

            _tex = new Texture2D(width, height, TextureFormat.Alpha8, mipChain: false);
            _tex.filterMode = filter;
            _tex.wrapMode = wrap;
            _tex.LoadRawTextureData(_alpha);
            _tex.Apply(updateMipmaps: false, makeNoLongerReadable: false);

            IsInitialized = true;
        }

        public void SetPixels(IEnumerable<(int x, int y)> activePixels)
        {
            if (!IsInitialized) 
                throw new InvalidOperationException("Not initialized");

            _nextActive.Clear();

            foreach (var (x0, y0) in activePixels)
            {
                if ((uint)x0 >= (uint)Width || (uint)y0 >= (uint)Height)
                    throw new ArgumentException($"Invalid pixel coordinates ({x0},{y0})");

                var y = _startCorner == StartCorner.BottomLeft 
                    ? y0 
                    : Height - 1 - y0;
                
                var idx = y * Width + x0;
                _nextActive.Add(idx);
            }

            var anyChange = false;

            AddPositiveDelta();
            SubtractNegativeDelta();

            _lastActive.Clear();
            foreach (var idx in _nextActive) 
                _lastActive.Add(idx);

            if (anyChange)
                _tex.Apply(updateMipmaps: false, makeNoLongerReadable: false);
            return;

            void AddPositiveDelta()
            {
                foreach (var idx in _nextActive)
                {
                    if (_lastActive.Contains(idx)) continue;

                    var y = idx / Width;
                    var x = idx - y * Width;

                    if (_alpha[idx] == 255) 
                        continue;
                
                    _alpha[idx] = 255;
                    _tex.SetPixel(x, y, _onColor); 
                    anyChange = true;
                }
            }

            void SubtractNegativeDelta()
            {
                foreach (var idx in _lastActive)
                {
                    if (_nextActive.Contains(idx)) 
                        continue;

                    var y = idx / Width;
                    var x = idx - y * Width;

                    if (_alpha[idx] == 0) 
                        continue;
                
                    _alpha[idx] = 0;
                    _tex.SetPixel(x, y, _offColor);
                    anyChange = true;
                }
            }
        }

        public void Clear()
        {
            if (!IsInitialized) 
                throw new InvalidOperationException("Not initialized");

            Array.Fill(_alpha, (byte)0);
            _lastActive.Clear();
            _nextActive.Clear();

            _tex.LoadRawTextureData(_alpha);
            _tex.Apply(updateMipmaps: false, makeNoLongerReadable: false);
        }

        public void Dispose()
        {
            if (_tex != null)
            {
                UnityEngine.Object.Destroy(_tex);
                _tex = null;
            }
            IsInitialized = false;
        }
    }
}
