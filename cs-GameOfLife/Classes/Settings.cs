﻿using System;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace cs_GameOfLife.Classes
{
    [Serializable]
    public class Settings
    {
        
        #region Properties

        public int XDim { get; set; }
        public int YDim { get; set; }
        public int Size { get; set; }
        public int Speed { get; set; }
        [XmlIgnore]
        public SolidBrush CellColor { get; set; }
        [XmlIgnore]
        public Pen BorderColor { get; set; }
        [XmlIgnore]
        public Color BackgroundColor { get; set; }

        public string CellColorHtml
        {
            get => ColorTranslator.ToHtml(CellColor.Color);
            set => CellColor.Color = ColorTranslator.FromHtml(value);
        }
        public string BorderColorHTML
        {
            get => ColorTranslator.ToHtml(BorderColor.Color);
            set => BorderColor.Color = ColorTranslator.FromHtml(value);
        }

        public string BackgroundColorHTML
        {
            get => ColorTranslator.ToHtml(BackgroundColor);
            set => BackgroundColor = ColorTranslator.FromHtml(value);
        }

        #endregion

        #region Constructors

        public Settings()
        {
            Default();
        }

        #endregion

        #region Methods

        public void Default()
        {
            XDim = 40;
            YDim = 40;
            Size = 10;
            Speed = 200;
            CellColor = new SolidBrush(Color.White);
            BorderColor = new Pen(Color.Gray) {Width = 0.1f};
            BackgroundColor = Color.Black;
        }

        public bool Load(string fileName)
        {
            if (!File.Exists(fileName))
                return false;

            Settings s;
            var serializer = new XmlSerializer(typeof(Settings));
            using (var reader = XmlReader.Create(fileName))
                s = (Settings) serializer.Deserialize(reader);

            XDim = s.XDim;
            YDim = s.YDim;
            Size = s.Size;
            Speed = s.Speed;
            CellColorHtml = s.CellColorHtml;
            BorderColorHTML = s.BorderColorHTML;
            return true;
        }

        public void Save(string fileName)
        {
            var xmlserializer = new XmlSerializer(typeof(Settings));
            using (var fileWriter = new FileStream(fileName, FileMode.Create))
                xmlserializer.Serialize(fileWriter, this);
        }

        #endregion

    }
}
