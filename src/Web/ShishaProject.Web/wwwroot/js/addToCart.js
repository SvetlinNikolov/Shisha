// Add to cart JS logic
// Variable definitions
let addToCartButtons = document.querySelectorAll('.product-add-to-cart-button');

// Function definitions
function addProductToCart() {
    // Do not execute if the button is disabled
    if (this.classList.contains('product-add-to-cart-button-disabled')) {
        return;
    }

    // Define variables
    let productParentNode = this.parentNode.parentNode;
    let packaging = null;
    let variationId;
    let quantity = 1;
    let productParent = productParentNode.attributes;

    // Check if the button is on the product page
    if (this.classList.contains('product-page')) {
        productParent = document.querySelector('.product-text-container').attributes;
        packaging = document.querySelector('.packaging-choices-container .packaging-choice.active').innerText;
        quantity = Number(document.getElementById('quantity').value);

        if (quantity < 1) {
            quantity = 1;
        }
    }

    if (!this.classList.contains('product-page')) {
        // Set packaging on products page
        let packagingChoicesContainer = productParentNode.querySelectorAll('.packaging-choices-container .packaging-choice');
        for (let packagingChoice of packagingChoicesContainer) {
            if (packagingChoice.classList) {
                if (packagingChoice.classList.contains('active')) {
                    packaging = packagingChoice.innerText;
                    variationId = packagingChoice.getAttribute('data-variation-id');
                    break;
                }
            }
        }

        // Set quantity on products page
        let getQuantity = productParentNode.querySelector('.quantity input').value;
        quantity = getQuantity;

        if (quantity < 1) {
            quantity = 1;
        }
    }

    let productAttributeObject = {};

    for (let attribute of productParent) {
        productAttributeObject[attribute.name] = attribute.value;
    }

    let productNumber = productAttributeObject['data-product-number'];


    let data = {
        'flavour_id': productNumber,
        quantity,
        'flavour_variation_id': variationId
    };

    console.log('Svetlio', data)
    postRequest('Cart/AddToCart', data)
        .then(data => {
            console.log(data);
        });
}

// Add event listeners
for (let button of addToCartButtons) {
    button.addEventListener('click', addProductToCart);
}
