// Cart JS logic
// Variable definitions
let addToCartButtons = document.querySelectorAll('.product-add-to-cart-button');

// Function definitions
function addProductToCart() {
    // Define variables
    let productParentNode = this.parentNode;
    let packaging = null;
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
        let packagingChoicesContainer = productParentNode.childNodes[1].childNodes[3].childNodes[1].childNodes[5].childNodes;
        for (let packagingChoice of packagingChoicesContainer) {
            if (packagingChoice.classList) {
                if (packagingChoice.classList.contains('active')) {
                    packaging = packagingChoice.innerText;
                    break;
                }
            }
        }

        // Set quantity on products page
        let getQuantity = productParentNode.childNodes[1].childNodes[3].childNodes[3].childNodes[1].childNodes[5].value;
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
        productNumber,
        packaging,
        quantity
    };

    // TO DO - SEND TO THE BE
    console.log(data);
    // postRequest('addUrl', { data })
    // .then(data => {
    //     console.log(data);
    // });
    // TO DO - SEND TO THE BE
}

// Add event listeners
for (let button of addToCartButtons) {
    button.addEventListener('click', addProductToCart);
}
