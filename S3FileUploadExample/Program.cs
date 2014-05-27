using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;


namespace S3Test
{

   \
    class Program
    {
        static string bucketName = "sheltdev";
       
        static string filepath = "C:\\Users\\Notebook\\Desktop\\SheltDev\\tiger.jpg";

        

        static void Main(string[] args)
        {
            
            try
            {
                TransferUtility fT = new TransferUtility(new AmazonS3Client(Amazon.RegionEndpoint.USEast1));
                string fileKey = genKey();
               

                TransferUtilityUploadRequest uR = new TransferUtilityUploadRequest
                {
                    BucketName = bucketName,
                    FilePath = filepath,
                    CannedACL = S3CannedACL.PublicRead,
                    Key = fileKey

                };
                
                uR.Metadata.Add("Title", "Tiger");
                fT.Upload(uR);
                Console.WriteLine("File Uploaded. Access \"S3.amazonaws.com/sheltdev/" + fileKey );
                Console.ReadKey(false);
            }

            catch (AmazonS3Exception e)
            {
                Console.WriteLine(e.Message, e.InnerException);
                Console.ReadKey(false);
            }

        }

        private static string genKey()
        {
            Random generator = new Random();

            String key = "";

            for (int i = 0; i < 4; i++)
                key += (char)(122 - generator.Next(26));
            for (int i = 0; i<6; i++)
                key += generator.Next(10);
       
            return key;
        }
    }
}
