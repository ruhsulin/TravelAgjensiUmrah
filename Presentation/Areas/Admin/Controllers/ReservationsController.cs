using Microsoft.AspNetCore.Mvc;
using TravelAgjensiUmrah.App.Constants;
using TravelAgjensiUmrah.App.Interfaces;

namespace Presentation.Areas.Admin.Controllers
{
    [Area(AreasConstants.Admin)]

    public class ReservationsController : Controller
    {
        private readonly IReservationRepository _reservationRepository;

        // KOnstruktori
        public ReservationsController(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        // Index
        public IActionResult Index()
        {
            return View();
        }


        // Get Reservations JSON
        [HttpGet]
        public IActionResult GetReservationsJson()
        {
            try
            {
                var reservation = _reservationRepository.GetAllReservations();
                var result = reservation.Select(x => new
                {
                    id = x.Id,
                    user = x.UserId,
                    package = x.PackageId,
                    reservationDate = x.BookingDate,
                    price = x.TotalPrice,
                    status = x.Status,
                    paymentMethod = x.PaymentMethod,
                });

                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // DELETE
        [HttpDelete]
        public async Task<IActionResult> DeleteReservationAsync(int id)
        {
            try
            {
                var reservation = _reservationRepository.GetReservationById(id);
                if (reservation != null)
                {
                    _reservationRepository.Delete(reservation);
                    _reservationRepository.SaveChanges();
                    return Ok(true);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data");
            }
        }
    }
}
