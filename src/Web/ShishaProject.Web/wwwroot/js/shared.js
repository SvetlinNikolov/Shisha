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
        if (quantityInputValue - 1 > 1) {
            quantityInput.value = quantityInputValue - 1;
        }
        else {
            quantityInput.value = 1;
        }
    }

    if (buttonAction === 'increment') {
        if (quantityInputValue + 1 < 99) {
            quantityInput.value = quantityInputValue + 1;
        }
        else {
            quantityInput.value = 99;
        }
    }

    // Product number related code
    let productParentNode = buttonElement.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode;
    let productParent = productParentNode.attributes;
    let productAttributeObject = {};
    for (let attribute of productParent) {
        productAttributeObject[attribute.name] = attribute.value;
    }
    let productNumber = productAttributeObject['data-product-id'];
    let variationId = productAttributeObject['data-product-variation-id'];

    let data = {
        'flavour_id': productNumber,
        'quantity': quantityInput.value,
        'flavour_variation_id': variationId
    };

    console.log('Svetlio', data)
    postRequest('Cart/AddToCart', data)
        .then(data => {
            console.log(data);
        });
}

// Add event listeners
for (let button of allDecrementButtons) {
    button.addEventListener('click', (e) => incrementOrDecrementQuantity(e.target, 'decrement'));
}

for (let button of allIncrementButtons) {
    button.addEventListener('click', (e) => incrementOrDecrementQuantity(e.target, 'increment'));
}
