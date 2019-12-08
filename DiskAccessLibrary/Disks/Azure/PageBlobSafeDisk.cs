using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;

namespace DiskAccessLibrary.Disks.Azure
{

    public class PageBlobSafeDisk : Disk
    {
        private const int DefaultBytesPerSector = 512;
        private CloudPageBlob m_cloudBlob;
        private long m_size;
        string m_containerName = "mirror";
        string m_blobName = "DiskImg.img";
        private string m_blobConnectionString;
        private ChunckBuffer m_chunckBuffer = new ChunckBuffer();

        private bool m_readOnly;

        public PageBlobSafeDisk(string connectionString, string container, string blob, bool readOnly)
        {
            m_blobConnectionString = connectionString;
            m_containerName = container; 
            m_blobName = blob;
            m_readOnly = readOnly;

            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(m_blobConnectionString);
            CloudBlobClient blobClient = cloudStorageAccount.CreateCloudBlobClient();
            CloudBlobContainer blobContainer = blobClient.GetContainerReference(m_containerName);
            m_cloudBlob = blobContainer.GetPageBlobReference(m_blobName);
            m_cloudBlob.FetchAttributes();
            m_size = m_cloudBlob.Properties.Length;
        }

        public string Description => $"Azure Blob:{m_containerName}/{m_blobName}";

        public override byte[] ReadSectors(long sectorIndex, int sectorCount)
        {
            return m_readOnly ? 
                ReadSectorsReadOnly(sectorIndex, sectorCount) : 
                ReadSectorsReadWrite(sectorIndex, sectorCount);
        }

        private byte[] ReadSectorsReadWrite(long sectorIndex, int sectorCount)
        {
            CheckBoundaries(sectorIndex, sectorCount);

            long offset = sectorIndex * BytesPerSector;
            byte[] result = new byte[BytesPerSector * sectorCount];

            using (Stream stream = m_cloudBlob.OpenRead())
            {
                stream.Seek(offset, SeekOrigin.Begin);
                stream.Read(result, 0, BytesPerSector * sectorCount);
            }

            return result;
        }

        private byte[] ReadSectorsReadOnly(long sectorIndex, int sectorCount)
        {
            CheckBoundaries(sectorIndex, sectorCount);

            long offset = sectorIndex * BytesPerSector;
            byte[] result = new byte[BytesPerSector * sectorCount];

            using (Stream stream = m_cloudBlob.OpenRead())
            {
                stream.Seek(offset, SeekOrigin.Begin);
                stream.Read(result, 0, BytesPerSector * sectorCount);
            }

            m_chunckBuffer.MergeChunks(result, (int) offset);
            return result;
        }

        public override void WriteSectors(long sectorIndex, byte[] data)
        {
            if (m_readOnly)
            {
                WriteSectorReadOnly(sectorIndex, data);
            }
            else
            {
                WriteSectorsReadWrite(sectorIndex, data);
            }
        }

        private void WriteSectorsReadWrite(long sectorIndex, byte[] data)
        {
            CheckBoundaries(sectorIndex, data.Length / this.BytesPerSector);
            long offset = sectorIndex * BytesPerSector;

            using (Stream stream = m_cloudBlob.OpenWrite(null))
            {
                stream.Seek(offset, SeekOrigin.Begin);
                stream.Write(data, 0, data.Length);
            }
        }
        private void WriteSectorReadOnly(long sectorIndex, byte[] data)
        {
            CheckBoundaries(sectorIndex, data.Length / this.BytesPerSector);
            long offset = sectorIndex * BytesPerSector;

            Chunck chunck = new Chunck() {offset = (int) offset, data = data};
            m_chunckBuffer.AddChunck(chunck);
        }

        private void CheckBoundaries(long sectorIndex, int sectorCount)
        {
            if (sectorIndex < 0 || sectorIndex + (sectorCount - 1) >= this.TotalSectors)
            {
                throw new ArgumentOutOfRangeException("Attempted to access data outside of disk");
            }
        }

        public override int BytesPerSector => DefaultBytesPerSector;
        public override long Size => m_size;
    }
}
