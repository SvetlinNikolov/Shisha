// Cookie info popup
// Variable definitions
let cookieInfoPopup = document.getElementById('cookie-info-popup');
let cookieInfoPopupCloseBtn = document.getElementById('cookie-info-popup-close');

// Function definitions
function cookieInfoPopupClose() {
    cookieInfoPopup.classList.add('cookie-info-popup-hidden');
}

// Add event listeners
if (cookieInfoPopupCloseBtn) {
    cookieInfoPopupCloseBtn.addEventListener('click', cookieInfoPopupClose);
}

// Quantity decrement and increment
// Shared for products, product and cart
// Variable definitions
let allDecrementButtons = document.querySelectorAll('.quantity-decrement');
let allIncrementButtons = document.querySelectorAll('.quantity-increment');

// Function definitions
function incrementOrDecrementQuantity(buttonElement, buttonAction) {
    let quantityContainer = buttonElement.parentNode;
    let quantityInput = quantityContainer.querySelector('input[name="quantity"]');
    let quantityInputValue = Number(quantityInput.value);

    if (buttonAction === 'decrement') {
        quantityInput.value = quantityInputValue - 1;
    }

    if (buttonAction === 'increment') {
        quantityInput.value = quantityInputValue + 1;
    }
}

// Add event listeners
for (let button of allDecrementButtons) {
    button.addEventListener('click', (e) => incrementOrDecrementQuantity(e.target, 'decrement'));
}

for (let button of allIncrementButtons) {
    button.addEventListener('click', (e) => incrementOrDecrementQuantity(e.target, 'increment'));
}
