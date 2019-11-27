using System.Collections.Generic;

namespace DiskAccessLibrary.Disks.Azure
{
    public class ChunckBuffer
    {
        private List<Chunck> chuncks = new List<Chunck>();

        public void AddChunck(Chunck chunck)
        {
            chuncks.Add(chunck);
        }

        public void MergeChunks(byte[] baseChunkBytes, int offset)
        {
            Chunck baseChunck = new Chunck(){offset = offset, data = baseChunkBytes};
            foreach (Chunck chunck in chuncks)
            {
                baseChunck.MergeFrom(chunck);
            }
        }
    }
}