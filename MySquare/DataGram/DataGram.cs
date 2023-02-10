using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySquare.DataGram {

    [Serializable] // 进程间 RPC 消息协议 ？ 包装成泛型
    public class DataGram<T> {

        public string cmd;
        public T data;
    }
}
