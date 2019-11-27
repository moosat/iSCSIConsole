using System;

namespace DiskAccessLibrary.Disks.Azure
{
    public class Chunck
    {
        public int offset;
        public byte[] data;

        public void MergeFrom(Chunck source)
        {
            int thisEndOffset = this.offset + this.data.Length - 1;
            int sourceEndOffset = source.offset + source.data.Length - 1;

            if (this.offset > sourceEndOffset)
                return;
            if (thisEndOffset < source.offset)
                return;

            int startOffset = Math.Max(this.offset, source.offset);
            int endOffset = Math.Min(thisEndOffset, sourceEndOffset);
            int bytesToCopy = endOffset - startOffset + 1;

            Array.ConstrainedCopy(source.data,
                startOffset - source.offset,
                this.data,
                startOffset - this.offset,
                bytesToCopy);
        }
    }
}