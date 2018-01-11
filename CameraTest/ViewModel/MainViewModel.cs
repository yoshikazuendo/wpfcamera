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
    /// MainWindow��ViewModel�ł��B
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region �v���p�e�B
        /// <summary>
        /// �v���r���[�̕�
        /// </summary>
        private int videoPreviewWidth;
        /// <summary>
        /// �v���r���[�̕����擾�E�ݒ肵�܂��B
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
        /// �v���r���[�̍���
        /// </summary>
        private int videoPreviewHeight;
        /// <summary>
        /// �v���r���[�̍������擾�E�ݒ肵�܂��B
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
        /// �I������Ă���J����
        /// </summary>
        private MediaInformation selectedVideoDevice;
        /// <summary>
        /// �I������Ă���J�������擾�E�ݒ肵�܂��B
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
        /// �B�����X�i�b�v�V���b�g
        /// </summary>
        private ImageSource snapshotTaken;
        /// <summary>
        /// �B�����X�i�b�v�V���b�g���擾�E�ݒ肵�܂��B
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
        /// �B����Bitmap�`���̃X�i�b�v�V���b�g
        /// </summary>
        private Bitmap snapshotBitmap;
        /// <summary>
        /// �B����Bitmap�`���̃X�i�b�v�V���b�g���擾�E�ݒ肵�܂��B
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
        /// �J�����̃��X�g
        /// </summary>
        private IEnumerable<MediaInformation> mediaDeviceList;
        /// <summary>
        /// �J�����̃��X�g���擾�E�ݒ肵�܂��B
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
        /// �R���X�g���N�^�ł��B
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
        /// Loaded�C�x���g�̃R�}���h
        /// </summary>
        private RelayCommand loadedCommand;
        /// <summary>
        /// Loaded�C�x���g�̃R�}���h���擾���܂��B
        /// </summary>
        public RelayCommand LoadedCommand
        {
            get
            {
                return loadedCommand ?? (loadedCommand = new RelayCommand(OnWindowLoaded));
            }
        }

        /// <summary>
        /// ��ʃ��[�h���̏������s���܂��B
        /// </summary>
        private void OnWindowLoaded()
        {
            MediaDeviceList = WebcamDevice.GetVideoDevices;
            SelectedVideoDevice = null;
        }

        #endregion

        #region �X�i�b�v�V���b�g���J�n

        /// <summary>
        /// �X�i�b�v�V���b�g���J�n�̃R�}���h
        /// </summary>
        private RelayCommand snapshotCommand;
        /// <summary>
        /// �X�i�b�v�V���b�g���J�n�̃R�}���h���擾���܂��B
        /// </summary>
        public RelayCommand SnapshotCommand
        {
            get
            {
                return snapshotCommand ?? (snapshotCommand = new RelayCommand(OnSnapshot));
            }
        }

        /// <summary>
        /// �X�i�b�v�V���b�g���J�n���̏������s���܂��B
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