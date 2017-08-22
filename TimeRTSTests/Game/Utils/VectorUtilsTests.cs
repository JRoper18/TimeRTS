using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using TimeRTS.Game.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeRTS.Game.Utils.Tests {
    [TestClass()]
    public class VectorUtilsTests {
        [TestMethod()]
        public void RotateAroundOriginTest() {
            Vector2 vec1 = VectorUtils.RotateAroundOrigin(new Vector2(3, 4), 90);
            Assert.AreSame(vec1.X, -4);
            Assert.AreSame(vec1.Y, 3);
        }
    }
}