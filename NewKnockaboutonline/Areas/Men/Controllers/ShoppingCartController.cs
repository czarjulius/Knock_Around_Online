using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using NewKnockaboutonline.Models;

namespace NewKnockaboutonline.Areas.Men.Controllers
{
    public class ShoppingCartController : Controller
    {
        private KnockaboutonlineEntities db;
        private string strCart = "Cart";
        public ShoppingCartController()
        {
            db = new KnockaboutonlineEntities();
        }
        // GET: Common/ShoppingCart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OrderNow(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (Session[strCart] == null)
            {
                List<Cart> lsCart = new List<Cart>
                {
                    new Cart(db.PRODUCT.Find(id), 1)
                };

                Session[strCart] = lsCart;
            }
            else
            {
                List<Cart> lsCart = (List<Cart>)Session[strCart];
                int check = IsExistingCheck(id);
                if (check == -1)
                    lsCart.Add(new Cart(db.PRODUCT.Find(id), 1));
                else
                    lsCart[check].Quantity++;

                Session[strCart] = lsCart;

            }
            return View("Index");
        }

        private int IsExistingCheck(int? id)
        {
            List<Cart> lsCart = (List<Cart>)Session[strCart];
            for (int i = 0; i < lsCart.Count; i++)
            {
                if (lsCart[i].Product.ProductId == id) return i;
            }
            return -1;
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int check = IsExistingCheck(id);
            List<Cart> lsCart = (List<Cart>)Session[strCart];
            lsCart.RemoveAt(check);
            return View("Index");
        }

        public ActionResult UpdateCart(FormCollection frc)
        {
            string[] quantities = frc.GetValues("quantity");
            List<Cart> lstCart = (List<Cart>)Session[strCart];
            for (int i = 0; i < lstCart.Count; i++)
            {
                lstCart[i].Quantity = Convert.ToInt32(quantities[i]);
            }
            Session[strCart] = lstCart;
            return View("Index");



        }

        public ActionResult CheckOut()
        {
            //    List<Cart> lstCart = (List<Cart>)Session[strCart];

            //        if (lstCart == null)
            //    {
            //        TempData["cartEmpty"] = "Your Cart is empty. Kindly add item(s) to your cart";
            //        return RedirectToAction("index", "Product");
            //    }

            return View("CheckOut");


        }

        public ActionResult ProcessOrder(FormCollection frc)
        {
            List<Cart> lstCart = (List<Cart>)Session[strCart];
            // Save the Order into Order Table
           



                    ORDER order = new ORDER()
                    {
                        CustomerName = frc["cusName"],
                        CustomerPhone = frc["cusPhone"],
                        CustomerEmail = frc["cusEmail"],
                        CustomerAddress = frc["cusAddress"],
                        OrderDate = DateTime.Now,
                        PaymentType = "Cash",
                        Status = "Processing"
                    };

                    Session["CustomerName"] = order.CustomerName;
                    Session["CustomerPhone"] = order.CustomerPhone;
                    Session["CustomerEmail"] = order.CustomerEmail;


                    db.ORDER.Add(order);
                    db.SaveChanges();
                    // Save the Order detail into Order Table

                    foreach (Cart cart in lstCart)
                    {
                        ORDER_DETAIL orderDetail = new ORDER_DETAIL()
                        {
                            OrderId = order.OrderId,
                            ProductId = cart.Product.ProductId,
                            Quantity = cart.Quantity,
                            Price = cart.Product.Price,
                        };

                        db.ORDER_DETAIL.Add(orderDetail);
                        db.SaveChanges();
                    }
                   
            


            try
            {

                if (ModelState.IsValid)
            {

                    //To the Owner
                    //what happens if for any reason, this email fails to send the first time
                MailMessage mail1 = new MailMessage();
                mail1.To.Add("Knockaboutsneakers@gmail.com");
                mail1.From = new MailAddress("Knockaboutsneakers@gmail.com");
                mail1.Subject = "New Order";

                StringBuilder Body1 = new StringBuilder();
                Body1.Append("Name :   " + Session["CustomerName"] + "<br>");
                //Body1.Append("Email :   " + _objModelMail.Email + "<br>");
                Body1.Append("Contact :   " + Session["CustomerPhone"]);

                mail1.Body = Body1.ToString();
                mail1.IsBodyHtml = true;
                SmtpClient smtp1 = new SmtpClient();
                smtp1.Host = "smtp.gmail.com";
                smtp1.Port = 587;
                smtp1.UseDefaultCredentials = false;
                smtp1.Credentials = new System.Net.NetworkCredential("Knockaboutsneakers@gmail.com", "Bubbles051982");//hardcoding password here will expose your app to security threat, encrypt and save it in database // Enter senders User name and password
                smtp1.EnableSsl = true;
                smtp1.Send(mail1);




                //To the Customer
                MailMessage mail = new MailMessage();
                mail.To.Add(Session["CustomerEmail"].ToString());
                mail.From = new MailAddress("Knockaboutsneakers@gmail.com");
                mail.Subject = "Successful Order Made";

                StringBuilder Body = new StringBuilder();
                Body.Append("<p>" + "Dear " + Session["CustomerName"] + "</p>" + "<br>");
                Body.Append("Your Request Have been recieved and will be proccessed Soon");


                mail.Body = Body.ToString();
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential
                    ("Knockaboutsneakers@gmail.com", "Bubbles051982");// Enter seders User name and password
                smtp.EnableSsl = true;
                smtp.Send(mail);
               
                //return RedirectToAction("Index");
            }
                else
                {
                    return View();
                }

                //Remove Shopping cart Session
                Session.Remove(strCart);
                return View("OrderSuccess");
            }




                catch (Exception ex)
                {
                    TempData["ErrorMsg"] = "Something went wrong. " + ex.Message;
                return View("CheckOut");

            }

        }






    }
}