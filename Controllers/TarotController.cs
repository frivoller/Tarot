using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TarotApp.Controllers
{
    public class TarotController : Controller
    {
        private static readonly List<Card> _deck = new List<Card>
        {
            // buyuk arkanalar
            new Card { Id = 0, Name = "Fool (Deli)", ImageUrl = "/images/cards/fool.jpg", 
                Meaning = "Yeni başlangıçlar, saflık, spontanelik, özgür ruh" },
            new Card { Id = 1, Name = "Magician (Sihirbaz)", ImageUrl = "/images/cards/magician.jpg", 
                Meaning = "Yaratıcılık, beceri, irade gücü, konsantrasyon" },
            new Card { Id = 2, Name = "High Priestess (Baş Rahibe)", ImageUrl = "/images/cards/highpriestess.jpg", 
                Meaning = "Sezgi, bilinçaltı, gizem, içsel bilgelik" },
            new Card { Id = 3, Name = "Empress (İmparatoriçe)", ImageUrl = "/images/cards/empress.jpg", 
                Meaning = "Bereket, annelik, doğa, bolluk" },
            new Card { Id = 4, Name = "Emperor (İmparator)", ImageUrl = "/images/cards/emperor.jpg", 
                Meaning = "Otorite, yapı, kontrol, babalık" },
            new Card { Id = 5, Name = "Hierophant (Hierofant)", ImageUrl = "/images/cards/hierophant.jpg", 
                Meaning = "Geleneksel değerler, maneviyat, öğretmenlik" },
            new Card { Id = 6, Name = "Lovers (Aşıklar)", ImageUrl = "/images/cards/lovers.jpg", 
                Meaning = "Aşk, uyum, ilişkiler, seçimler" },
            new Card { Id = 7, Name = "Chariot (Savaş Arabası)", ImageUrl = "/images/cards/chariot.jpg", 
                Meaning = "İlerleme, kararlılık, zafer, irade" },
            new Card { Id = 8, Name = "Strength (Güç)", ImageUrl = "/images/cards/strength.jpg", 
                Meaning = "Güç, cesaret, sabır, kontrol" },
            new Card { Id = 9, Name = "Hermit (Ermiş)", ImageUrl = "/images/cards/hermit.jpg", 
                Meaning = "İçe dönüş, yalnızlık, rehberlik, bilgelik" },
            new Card { Id = 10, Name = "Wheel of Fortune (Kader Çarkı)", ImageUrl = "/images/cards/wheeloffortune.jpg", 
                Meaning = "Değişim, şans, kader, dönüm noktaları" },
            new Card { Id = 11, Name = "Justice (Adalet)", ImageUrl = "/images/cards/justice.jpg", 
                Meaning = "Adalet, denge, doğruluk, karma" },
            new Card { Id = 12, Name = "Hanged Man (Asılan Adam)", ImageUrl = "/images/cards/hangedman.jpg", 
                Meaning = "Fedakarlık, bekleyiş, teslim olma, yeni perspektif" },
            new Card { Id = 13, Name = "Death (Ölüm)", ImageUrl = "/images/cards/death.jpg", 
                Meaning = "Son, değişim, dönüşüm, yenilenme" },
            new Card { Id = 14, Name = "Temperance (Denge)", ImageUrl = "/images/cards/temperance.jpg", 
                Meaning = "Denge, uyum, ılımlılık, sabır" },
            new Card { Id = 15, Name = "Devil (Şeytan)", ImageUrl = "/images/cards/devil.jpg", 
                Meaning = "Bağımlılık, maddesellik, tutku, kısıtlanma" },
            new Card { Id = 16, Name = "Tower (Kule)", ImageUrl = "/images/cards/tower.jpg", 
                Meaning = "Ani değişim, yıkım, aydınlanma, özgürleşme" },
            new Card { Id = 17, Name = "Star (Yıldız)", ImageUrl = "/images/cards/star.jpg", 
                Meaning = "Umut, ilham, rehberlik, iyimserlik" },
            new Card { Id = 18, Name = "Moon (Ay)", ImageUrl = "/images/cards/moon.jpg", 
                Meaning = "Yanılsama, sezgiler, bilinçaltı, korkular" },
            new Card { Id = 19, Name = "Sun (Güneş)", ImageUrl = "/images/cards/sun.jpg", 
                Meaning = "Mutluluk, başarı, canlılık, aydınlanma" },
            new Card { Id = 20, Name = "Judgement (Mahşer)", ImageUrl = "/images/cards/judgement.jpg", 
                Meaning = "Yeniden doğuş, iç hesaplaşma, affetme" },
            new Card { Id = 21, Name = "World (Dünya)", ImageUrl = "/images/cards/world.jpg", 
                Meaning = "Tamamlanma, bütünlük, başarı, seyahat" }
        };

        private static Reading _currentReading = new Reading();

        public IActionResult Index()
        {
            _currentReading = new Reading();
            return View(_currentReading);
        }

        [HttpPost]
        public IActionResult SelectCard(string position)
        {
            if (_currentReading.SelectedCards.Count >= 3)
                return Json(new { success = false, message = "Zaten 3 kart seçildi" });

            var availableCards = _deck.Where(c => !_currentReading.SelectedCards.Any(sc => sc.Id == c.Id)).ToList();
            var randomCard = availableCards[new Random().Next(availableCards.Count)];
            randomCard.Position = position;
            randomCard.IsRevealed = false;

            _currentReading.SelectedCards.Add(randomCard);

            return Json(new { 
                success = true, 
                selectedCard = randomCard,
                cardsSelected = _currentReading.SelectedCards.Count 
            });
        }

        [HttpPost]
        public IActionResult RevealCards()
        {
            foreach (var card in _currentReading.SelectedCards)
            {
                card.IsRevealed = true;
            }
            _currentReading.IsCompleted = true;

            return Json(new { success = true, cards = _currentReading.SelectedCards });
        }

        [HttpGet]
        public IActionResult GetSelectedCards()
        {
            return Json(_currentReading.SelectedCards);
        }
    }
}
