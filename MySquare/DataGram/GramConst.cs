using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySquare.DataGram {
    // 这里定义为类，一般不常用，一般定义为enum？ 就是几种消息的封闭类型
    public class GramConst {
        public const string LAN_GAME_CREATED = "LAN_GAME_CREATED";
        public const string SQUARE_DATA = "SQUARE_DATA";
        public const string REMOTE_CONNECTED = "CLIENT_CONNECTED";
        public const string GAME_OVER = "GAME_OVER";
        public const string MAGIC_TOOL = "MAGIC_TOOL";
        public const string SCORE_CHANGE = "SCORE_CHANGE";
    }
}
