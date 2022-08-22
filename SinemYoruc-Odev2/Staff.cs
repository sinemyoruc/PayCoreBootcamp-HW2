using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SinemYoruc_Odev2
{
    public class Staff
    {
        public long Id { get; set; }

        //Name alani 20 ve 120 karakter arasinda olmali
        [StringLength(maximumLength: 120, MinimumLength = 3, ErrorMessage = "Name must be range in 3-120")]
        //Rakamlara ve ozel karakterlere izin verilmiyor, bosluklara izin veriliyor
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = ("Please do not enter numbers and special characters"))]
        public string Name { get; set; }


        //Lastname alani 20 ve 120 karakter arasinda olmali
        [StringLength(maximumLength: 120, MinimumLength = 3, ErrorMessage = "Lastname must be range in 3-120")]
        //Rakamlara ve ozel karakterlere izin verilmiyor, bosluklara izin veriliyor
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = ("Please do not enter numbers and special characters"))]
        public string Lastname { get; set; }


        //01-01-1989 formati icin yapildi
        [RegularExpression(@"[0-9]{2}[\-\/\.][0-9]{2}[\/\-\.][0-9]{2,4}", ErrorMessage = "Please enter valid date")]
        //dogum tarihi 11/11/1945 ve 10/10/2002 arasinda olmali
        [Range(typeof(DateTime), "11/11/1945", "10/10/2002")]
        [Required]
        public string DateOfBirth { get; set; }


        [Required]
        [EmailAddress]
        //@, nokta ve harf haric karakter girilmemesini sagliyor
        [RegularExpression(@"^[a-zA-Z]+\@+[a-zA-Z]+\.+[a-zA-Z]*$", ErrorMessage = ("Please enter valid email"))]
        public string Email { get; set; }



        [Required]
        //Telefon numarasının +905 ile baslayıp 9 rakam daha ekleme validasyonu
        [RegularExpression(@"^[+]905[0-9]{9}$", ErrorMessage = ("Please enter valid phone number"))]
        [Phone(ErrorMessage = "Phone is not valid.")]
        public string PhoneNumber { get; set; }



        //Maas 2000 ve 9000 arasinda olmali
        [Range(minimum: 2000, maximum: 9000, ErrorMessage = "Salary must be range in 2000-9000")]
        public double Salary { get; set; }
    }
}
