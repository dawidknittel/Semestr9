using System;
using System.Drawing;
using System.Windows.Forms;

namespace Kolejki_LAB3.Model
{
    public class PercentProgressBar : ProgressBar
    {
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == 0x000F)
            {
                var flags = TextFormatFlags.VerticalCenter |
                            TextFormatFlags.HorizontalCenter |
                            TextFormatFlags.SingleLine |
                            TextFormatFlags.WordEllipsis;

                string stateName = Value != 0 ? "Trwa obsługa..." : "Odczekiwanie...";

                TextRenderer.DrawText(CreateGraphics(), stateName, Font, new Rectangle(0, 0, this.Width, this.Height), Color.Black, flags);
            }
        }
    }
}
