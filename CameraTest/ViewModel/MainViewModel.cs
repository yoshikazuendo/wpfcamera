using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using CameraTest.Video;
using System.Windows.Media;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using System.Drawing.Imaging;

namespace CameraTest.ViewModel
{
    /// <summary>
    /// MainWindowのViewModelです。
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region プロパティ
        /// <summary>
        /// プレビューの幅
        /// </summary>
        private int videoPreviewWidth;
        /// <summary>
        /// プレビューの幅を取得・設定します。
        /// </summary>
        public int VideoPreviewWidth
        {
            get
            {
                return videoPreviewWidth;
            }

            set
            {
                videoPreviewWidth = value;
                RaisePropertyChanged(() => VideoPreviewWidth);
            }
        }

        /// <summary>
        /// プレビューの高さ
        /// </summary>
        private int videoPreviewHeight;
        /// <summary>
        /// プレビューの高さを取得・設定します。
        /// </summary>
        public int VideoPreviewHeight
        {
            get
            {
                return videoPreviewHeight;
            }

            set
            {
                videoPreviewHeight = value;
                RaisePropertyChanged(() => VideoPreviewHeight);
            }
        }

        /// <summary>
        /// 選択されているカメラ
        /// </summary>
        private MediaInformation selectedVideoDevice;
        /// <summary>
        /// 選択されているカメラを取得・設定します。
        /// </summary>
        public MediaInformation SelectedVideoDevice
        {
            get
            {
                return selectedVideoDevice;
            }

            set
            {
                selectedVideoDevice = value;
                RaisePropertyChanged(() => SelectedVideoDevice);
            }
        }

        /// <summary>
        /// 撮ったスナップショット
        /// </summary>
        private ImageSource snapshotTaken;
        /// <summary>
        /// 撮ったスナップショットを取得・設定します。
        /// </summary>
        public ImageSource SnapshotTaken
        {
            get
            {
                return snapshotTaken;
            }

            set
            {
                snapshotTaken = value;
                RaisePropertyChanged(() => SnapshotTaken);
            }
        }

        /// <summary>
        /// 撮ったBitmap形式のスナップショット
        /// </summary>
        private Bitmap snapshotBitmap;
        /// <summary>
        /// 撮ったBitmap形式のスナップショットを取得・設定します。
        /// </summary>
        public Bitmap SnapshotBitmap
        {
            get
            {
                return snapshotBitmap;
            }

            set
            {
                snapshotBitmap = value;
                RaisePropertyChanged(() => SnapshotBitmap);
            }
        }

        /// <summary>
        /// カメラのリスト
        /// </summary>
        private IEnumerable<MediaInformation> mediaDeviceList;
        /// <summary>
        /// カメラのリストを取得・設定します。
        /// </summary>
        public IEnumerable<MediaInformation> MediaDeviceList
        {
            get
            {
                return mediaDeviceList;
            }

            set
            {
                mediaDeviceList = value;
                RaisePropertyChanged(() => MediaDeviceList);
            }
        }
        #endregion

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
        }

        #region Loaded

        /// <summary>
        /// Loadedイベントのコマンド
        /// </summary>
        private RelayCommand loadedCommand;
        /// <summary>
        /// Loadedイベントのコマンドを取得します。
        /// </summary>
        public RelayCommand LoadedCommand
        {
            get
            {
                return loadedCommand ?? (loadedCommand = new RelayCommand(OnWindowLoaded));
            }
        }

        /// <summary>
        /// 画面ロード時の処理を行います。
        /// </summary>
        private void OnWindowLoaded()
        {
            MediaDeviceList = WebcamDevice.GetVideoDevices;
            SelectedVideoDevice = null;
        }

        #endregion

        #region スナップショットを開始

        /// <summary>
        /// スナップショットを開始のコマンド
        /// </summary>
        private RelayCommand snapshotCommand;
        /// <summary>
        /// スナップショットを開始のコマンドを取得します。
        /// </summary>
        public RelayCommand SnapshotCommand
        {
            get
            {
                return snapshotCommand ?? (snapshotCommand = new RelayCommand(OnSnapshot));
            }
        }

        /// <summary>
        /// スナップショットを開始時の処理を行います。
        /// </summary>
        private void OnSnapshot()
        {
            this.SnapshotTaken = ConvertToImageSource(SnapshotBitmap);
        }

        /// <summary>
        /// The convert to image source.
        /// </summary>
        /// <param name="bitmap"> The bitmap. </param>
        /// <returns> The <see cref="object"/>. </returns>
        public static ImageSource ConvertToImageSource(Bitmap bitmap)
        {
            var imageSourceConverter = new ImageSourceConverter();
            using (var memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, ImageFormat.Png);
                bitmap.Save(DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".png", ImageFormat.Png);
                var test = WebcamDevice.GetVideoDevices;

                var snapshotBytes = memoryStream.ToArray();
                return (ImageSource)imageSourceConverter.ConvertFrom(snapshotBytes); ;
            }
        }

        #endregion

    }
}