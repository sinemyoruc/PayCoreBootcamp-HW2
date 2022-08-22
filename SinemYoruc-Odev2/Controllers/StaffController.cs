using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SinemYoruc_Odev2.Controllers
{

    [Route("[controller]s")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        /*private static List<Staff> list = new List<Staff>    //1. yöntem direkt ana sinifin icinde liste olusturup eleman ekleme
        {
            new Staff
            {
                Id = 1,
                Name = "Deny",
                Lastname = "Sellen",
                DateOfBirth = new DateTime(1989, 01, 01).ToShortDateString(),
                Email = "deny@gmail.com",
                PhoneNumber = "+90555443366",
                Salary = 4450
            },

            new Staff {
                Id = 2,
                Name = "Ashley",
                Lastname = "Brown",
                DateOfBirth = new DateTime(1970, 10, 09).ToShortDateString(),
                Email = "ashley@gmail.com",
                PhoneNumber = "+905578962145",
                Salary = 4250
            },

            new Staff {
                Id = 3,
                Name = "Alex",
                Lastname = "Hunter",
                DateOfBirth = new DateTime(1995, 03, 02).ToShortDateString(),
                Email = "alex@gmail.com",
                PhoneNumber = "+90554856215",
                Salary = 5550
            }
        };
        */


        private static List<Staff> list = new(); //Staff turunde yeni nesne olusturuldu
        public StaffController() //2.yöntem constructor icinde listenin uzunlugunu kontrol edip eleman elemek
        {
            if (list.Count == 0)
            {
                list.Add(new Staff
                {
                    Id = 1,
                    Name = "Deny",
                    Lastname = "Sellen",
                    DateOfBirth = new DateTime(1989, 01, 01).ToShortDateString(),
                    Email = "deny@gmail.com",
                    PhoneNumber = "+90555443366",
                    Salary = 4450
                });
                list.Add(new Staff
                {
                    Id = 2,
                    Name = "Ashley",
                    Lastname = "Brown",
                    DateOfBirth = new DateTime(1970, 10, 09).ToShortDateString(),
                    Email = "ashley@gmail.com",
                    PhoneNumber = "+905578962145",
                    Salary = 4250
                });
                list.Add(new Staff
                {
                    Id = 3,
                    Name = "Alex",
                    Lastname = "Hunter",
                    DateOfBirth = new DateTime(1995, 03, 02).ToShortDateString(),
                    Email = "alex@gmail.com",
                    PhoneNumber = "+90554856215",
                    Salary = 5550
                });
            }
        }

        private ActionResult<List<Staff>> GetList() //Listenin elemanlarini almasi icin genel bir method yazildi
        {
            return new ActionResult<List<Staff>>(list);
        }


        [HttpGet]
        [Route("GetAll")]
        public ActionResult<List<Staff>> GetAll() //Listenin tum elemanlarini getiren Get methodu 
        {
            return GetList();
        }


        [HttpGet("GetById/{id}")]
        public ActionResult<Staff> GetById([FromRoute] long id) //id'ye gore listenin istenen elemanini getiren GetById methodu
        {
            Staff staff = list.Where(x => x.Id == id).FirstOrDefault(); //staffin id'si alindi
            if (staff == null)
            {
                return NotFound("Staff is not found");
            }
            return new ActionResult<Staff>(staff); //id'si alinan staff donduruldu
        }


        [HttpPost]
        public ActionResult<List<Staff>> CreateStaff([FromBody] Staff staff) //Yeni staff ekleyen Post methodu
        {
            var staffControl = list.SingleOrDefault(x => x.Id == staff.Id); //girilen id ile listedeki id esit mi?
            if (staffControl == null) //girilen idye sahip staff yoksa 
            {
                list.Add(staff); //yeni staffı listeye ekle
                return new ActionResult<List<Staff>>(list); //listeyi dondur
            }
            else
            {  //girilen idye sahip staff varsa 
                return BadRequest("This staff is already exist"); //hata dondur
            }
        }

        [HttpPut]
        public ActionResult<List<Staff>> UpdateStaff(int id, [FromBody] Staff updateStaff) //Staff guncelleyen Put methodu
        {
            Staff staff = list.Where(x => x.Id == id).FirstOrDefault(); //Guncellenecek staffin idsi alindi
            if (staff == null) //idsi verilen staff mevcut degilse hata dondur
            {
                return NotFound("Staff is not found");
            }
            else //verilen parametrelerin degerleri default degerlere esit degilse guncelleme yapiyoruz
            {
                var staffControl = list.SingleOrDefault(staff => staff.Id == updateStaff.Id); //guncellenecek id ile listedeki id esit mi?
                if (staffControl == null) //guncellenecek idye sahip staff yoksa 
                {
                    staff.Id = updateStaff.Id != default ? updateStaff.Id : staff.Id;     //gercek bir uygulamada id degismemeli fakat parametre olarak geldigi icin onu da guncelledim
                    staff.Name = updateStaff.Name != default ? updateStaff.Name : staff.Name;
                    staff.Lastname = updateStaff.Lastname != default ? updateStaff.Lastname : staff.Lastname;
                    staff.DateOfBirth = updateStaff.DateOfBirth != default ? updateStaff.DateOfBirth : staff.DateOfBirth;
                    staff.Email = updateStaff.Email != default ? updateStaff.Email : staff.Email;
                    staff.PhoneNumber = updateStaff.PhoneNumber != default ? updateStaff.PhoneNumber : staff.PhoneNumber;
                    staff.Salary = updateStaff.Salary != default ? updateStaff.Salary : staff.Salary;
                    return new ActionResult<List<Staff>>(list.ToList()); //Yeni liste donduruldu
                }
                else  //guncellenecek idye sahip staff varsa 
                {
                    return BadRequest("This staff is already exist");
                }
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<List<Staff>> DeleteStaff([FromRoute] int id)
        {
            Staff staff = list.Where(x => x.Id == id).FirstOrDefault(); //silinecek staffin idsi alindi
            if (staff == null) //idsi verilen staff mevcut degilse hata dondur
            {
                return NotFound("Staff is not found");
            }
            else
            {
                list.Remove(staff); //silme islemi
                return new ActionResult<List<Staff>>(list); //Yeni liste donduruldu
            }

        }
    }
}
