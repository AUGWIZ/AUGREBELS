using System;
using UnityEngine;
using ZXing;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

#if UNITY_ANDROID
using UnityEngine.Android;
#endif

public class QRCodeScanner : MonoBehaviour
{
    [SerializeField] private RawImage _rawImageBackground;
    [SerializeField] private AspectRatioFitter _aspectRatioFitter;
    [SerializeField] private TextMeshProUGUI _textOut;
    [SerializeField] private RectTransform _scanZone;

    private WebCamTexture _cameraTexture;
    private bool _isCamAvailable;
    private IBarcodeReader _barcodeReader;

    [SerializeField] private GameObject AUGREBELlogo;
    private const string QRKey = "QRScanned";
    private float _scanCooldown = 1.0f;
    private float _nextScanTime = 0f;

    private void Start()
    {
        RequestCameraPermission();

        // ✅ Check if QR was already scanned
        if (PlayerPrefs.GetInt(QRKey) == 1)
        {
            SceneManager.LoadScene("1MainScene");
            return;
        }
        
        AUGREBELlogo.SetActive(false);
        
        _barcodeReader = new BarcodeReader
        {
            AutoRotate = true,
            TryHarder = true
        };

        StartCoroutine(StartCameraAfterDelay());
    }

    private void Update()
    {
        UpdateCameraRender();

        if (_isCamAvailable && Time.time >= _nextScanTime)
        {
            Scan();
            _nextScanTime = Time.time + _scanCooldown;
        }
    }

    private void RequestCameraPermission()
    {
#if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
        }
#endif
    }

    private IEnumerator StartCameraAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        SetUpCamera();
    }

    private void SetUpCamera()
    {
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            _isCamAvailable = false;
            _textOut.text = "No camera found";
            return;
        }

        for (int i = 0; i < devices.Length; i++)
        {
            if (!devices[i].isFrontFacing)
            {
                _cameraTexture = new WebCamTexture(devices[i].name, 640, 480);
                break;
            }
        }

        if (_cameraTexture == null)
        {
            _textOut.text = "No suitable back camera found";
            return;
        }

        _cameraTexture.Play();
        _rawImageBackground.texture = _cameraTexture;
        _isCamAvailable = true;
    }

    private void UpdateCameraRender()
    {
        if (!_isCamAvailable) return;

        float ratio = (float)_cameraTexture.width / _cameraTexture.height;
        _aspectRatioFitter.aspectRatio = ratio;

        int orientation = _cameraTexture.videoRotationAngle;
        _rawImageBackground.rectTransform.localEulerAngles = new Vector3(0, 0, -orientation);
    }

    private void Scan()
    {
        if (_cameraTexture == null || !_cameraTexture.isPlaying || !_cameraTexture.didUpdateThisFrame)
        {
            return;
        }

        try
        {
            var colors = _cameraTexture.GetPixels32();
            var width = _cameraTexture.width;
            var height = _cameraTexture.height;

            Result result = _barcodeReader.Decode(colors, width, height);
            if (result != null)
            {
                _textOut.text = result.Text;
                Debug.Log("QR Code Detected: " + result.Text);

                if (result.Text == "https://www.augwiz.com/")
                {
                    PlayerPrefs.SetInt(QRKey, 1); // ✅ Store as int
                    PlayerPrefs.Save();
                    _textOut.text = "found AUGSTONE";
                    SceneManager.LoadScene("1MainScene");
                }
            }
            else
            {
                _textOut.text = "Scanning...";
            }
        }
        catch (System.Exception ex)
        {
            _textOut.text = "Scan Error";
            Debug.LogWarning("QR scanning failed: " + ex.Message);
        }
    }

    private void OnDestroy()
    {
        if (_cameraTexture != null && _cameraTexture.isPlaying)
        {
            _cameraTexture.Stop();
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.Save(); // ✅ Ensure PlayerPrefs is saved
    }

    // ✅ Optional method for dev/testing: reset scan status
    public void ResetQRScan()
    {
        PlayerPrefs.DeleteKey(QRKey);
        PlayerPrefs.Save();
    }
}
