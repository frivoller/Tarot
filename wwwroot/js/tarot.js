$(document).ready(function() {
    const cardsContainer = $('#cardsContainer');
    const cardWidth = 220; // card width + margin
    let currentPosition = 0;
    let selectedPositions = [];
    
    $('#prevCard').click(function() {
        if (currentPosition > 0) {
            currentPosition--;
            cardsContainer.scrollLeft(currentPosition * cardWidth);
        }
    });

    $('#nextCard').click(function() {
        if (currentPosition < 21) {
            currentPosition++;
            cardsContainer.scrollLeft(currentPosition * cardWidth);
        }
    });

    $('#selectCard').click(function() {
        if (selectedPositions.length >= 3) {
            alert('Zaten üç kart seçtiniz!');
            return;
        }

        const positions = ['Past', 'Present', 'Future'];
        const currentPosition = positions[selectedPositions.length];
        
        $.post('/Tarot/SelectCard', { position: currentPosition }, function(response) {
            if (response.success) {
                selectedPositions.push(currentPosition);
                
                const cardElement = $(`.selected-card[data-position="${currentPosition}"]`);
                cardElement.html(`
                    <div class="card-container">
                        <div class="card-inner">
                            <div class="card-front">
                                <img src="/images/cards/back.jpg" alt="Kart Arkası" class="img-fluid">
                            </div>
                        </div>
                    </div>
                `).addClass('has-card');

                if (selectedPositions.length === 3) {
                    $('#selectCard').hide();
                    $('#revealCards').show();
                }
            }
        });
    });

    $('#revealCards').click(function() {
        $.post('/Tarot/RevealCards', function(response) {
            if (response.success) {
                displayRevealedCards(response.cards);
            }
        });
    });

    function displayRevealedCards(cards) {
        cards.forEach(function(card) {
            const cardElement = $(`.selected-card[data-position="${card.position}"]`);
            cardElement.html(`
                <div class="card-container">
                    <div class="card-inner revealed">
                        <img src="${card.imageUrl}" alt="${card.name}" class="img-fluid">
                    </div>
                    <div class="card mt-3">
                        <div class="card-body">
                            <h5 class="card-title">${card.name}</h5>
                            <h6 class="card-subtitle mb-2 text-muted">${getPositionText(card.position)}</h6>
                            <p class="card-text">${card.meaning}</p>
                        </div>
                    </div>
                </div>
            `);
        });
    }

    function getPositionText(position) {
        switch(position) {
            case 'Past': return 'Geçmiş';
            case 'Present': return 'Şimdi';
            case 'Future': return 'Gelecek';
            default: return position;
        }
    }
});
