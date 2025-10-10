using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Libs.OneBitDisplay
{
    [RequireComponent(typeof(Image))]
    public sealed class OneBitImageDisplay : OneBitDisplay
    {
        [Header("Appearance")]
        [SerializeField] private Color _tint = Color.white;

        [Header("Coord System")]
        [SerializeField] private OneBitTexture.StartCorner _startCorner = OneBitTexture.StartCorner.TopLeft;

        [Header("UI")]
        [SerializeField] private float _pixelsPerUnit = 10f;

        private Image _image;
        private OneBitTexture _texture;
        private Sprite _sprite;
        private bool _initialized;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _texture = new OneBitTexture();
        }

        public override void Initialize(int width, int height)
        {
            if (_initialized) 
                throw new InvalidOperationException("Already initialized");

            _texture.Initialize(width, height, _startCorner);
            _sprite = Sprite.Create(
                _texture.Texture,
                new Rect(0, 0, width, height),
                pivot: new Vector2(0.5f, 0.5f),
                _pixelsPerUnit,
                extrude: 0,
                SpriteMeshType.FullRect
            );

            _image.sprite = _sprite;
            _image.color = _tint;
            _initialized = true;
        }

        public override void SetPixels(IEnumerable<(int x, int y)> activePixels)
        {
            if (!_initialized) 
                throw new InvalidOperationException("Not initialized");
            
            _texture.SetPixels(activePixels);
        }

        public void Clear()
        {
            if (!_initialized) 
                throw new InvalidOperationException("Not initialized");
            
            _texture.Clear();
        }

        private void OnDestroy()
        {
            if (_sprite != null)
            {
                Destroy(_sprite);
                _sprite = null;
            }
            _texture?.Dispose();
        }
    }
}