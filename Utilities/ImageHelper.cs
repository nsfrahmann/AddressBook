using AddressBook.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBook.Utilities
{
    public class ImageHelper
    {
        public static string GetImage(Profile profile)
        {
            var binary = Convert.ToBase64String(profile.Image);
            var ext = Path.GetExtension(profile.FileName);
            string imageDataURL = $"data:image/{ext};base64,{binary}";
            return imageDataURL;
        }

        public static string GetImage(PersonalAddress personalAddress)
        {
            var binary = Convert.ToBase64String(personalAddress.Image);
            var ext = Path.GetExtension(personalAddress.FileName);
            string imageDataURL = $"data:image/{ext};base64,{binary}";
            return imageDataURL;
        }
    }
}
