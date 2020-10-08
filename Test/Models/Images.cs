

namespace Test.Models
{
    public partial class Images
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public byte[] ImageData { get; set; }

        public virtual Users User { get; set; }
    }
}
