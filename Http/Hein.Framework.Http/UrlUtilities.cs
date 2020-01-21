using System.Collections.Generic;

namespace Hein.Framework.Http
{
    public static class Url
    {
        public static string Combine(params string[] pieces)
        {
            var pieceList = new List<string>();

            foreach (var piece in pieces)
            {
                pieceList.Add(piece.Trim().TrimEnd('/').TrimStart('/').Trim());
            }

            return string.Join("/", pieceList);
        }
    }
}
