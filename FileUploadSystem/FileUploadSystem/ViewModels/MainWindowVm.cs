using FileUploadSystem.TCPSocket;

namespace FileUploadSystem.ViewModels
{
    using System;
    using GalaSoft.MvvmLight;
    using System.Collections.Generic;
    using System.Net.Sockets;
    using System.Net;
    using System.Threading;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using System.Text;
    using Microsoft.Win32;
    using System.IO;
    using Microsoft.WindowsAPICodePack.Dialogs;
    using System.ComponentModel;

    // Base class for MainWindow's ViewModels. All methods must be virtual. Default constructor must exist.
    //  Using this Base Class will allow xaml to bind variables to a concrete View Model at compile time
    public class MainWindowVm : ViewModelBase, IDisposable, INotifyPropertyChanged
    {
        #region 构造、析构函数

        public MainWindowVm()
        {
            InitData();
        }

        public virtual void Dispose()
        {

        }

        #endregion

        #region 私有变量

        /// <summary>
        /// 用来存放连接服务的客户端的IP地址和端口号，对应的Socket
        /// </summary>
        private IDictionary<string, Socket> dicSocket;

        #endregion

        #region 属性

        private string pathStr;
        public string PathStr
        {
            get { return pathStr; }
            set
            {
                pathStr = value;
                OnPropertyChanged("PathStr");
            }
        }

        private string ip;
        public string Ip
        {
            get { return ip; }
            set
            {
                ip = value;
                OnPropertyChanged("Ip");
            }
        }

        #endregion

        #region 命令

        public ICommand PathCommand
        {
            get { return new RelayCommand(PathAction); }
        }

        public ICommand BeginOrStopBtnCommand
        {
            get { return new RelayCommand(BeginOrStopBtnAction); }
        }

        #endregion

        #region 函数

        private void InitData()
        {

        }

        private void PathAction()
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;//设置为选择文件夹
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                PathStr = dialog.FileName;
            }
        }

        private void BeginOrStopBtnAction()
        {
            var socket = new SocketManager(200, 1024);
            socket.Init();
            socket.Start(new IPEndPoint(IPAddress.Any, 13909));
        }


        #endregion

        #region 通知

        public new event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
