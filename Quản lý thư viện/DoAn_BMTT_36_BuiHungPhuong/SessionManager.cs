using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DoAn_BMTT_36_BuiHungPhuong
{
    public class SessionManager
    {
        private static Dictionary<string, string> sessions = new Dictionary<string, string>();

        public static void SaveSession(string userId, string sessionId)
        {
            // Kiểm tra xem userId đã có trong sessions chưa
            if (sessions.ContainsKey(userId))
            {
                // Nếu có, xóa phiên hiện tại
                RemoveSession(userId);
            }
            // Lưu sessionId cho userId
            sessions[userId] = sessionId;
        }

        public static bool IsSessionValid(string userId, string sessionId)
        {
            if (sessions.TryGetValue(userId, out var storedSessionId))
            {
                return storedSessionId == sessionId; // So sánh sessionId
            }
            return false; // Nếu không tìm thấy userId
        }

        public static void RemoveSession(string userId)
        {
            // Xóa sessionId cho userId
            sessions.Remove(userId);
        }
    }
}
