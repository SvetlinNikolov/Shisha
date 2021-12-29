// Requests to BE JS logic
// POST
async function postRequest(url = '', data = {}) {

    const response = await fetch(url, {
        method: 'POST',
        cache: 'no-cache',
        credentials: 'same-origin',
        headers: {
            'Content-Type': 'application/json',
        },
        referrerPolicy: 'no-referrer',
        body: JSON.stringify(data)
    });

    return response.json();
}

async function postRequestHTML(url = '', data = {}) {
    return await $.ajax({
        url: url,
        method: 'POST',
        data: JSON.stringify(data),
        headers: {
            'Content-Type': 'application/json',
        },
    }).done(function (data) {
        // Create modal and populate it with the response data
        return data;
    });
}

// POST - how to use
// postRequest('addUrl', { data })
// .then(data => {
//     console.log(data);
// });

// GET
async function getRequest(url = '') {
    const response = await fetch(url);

    return response.json();
}

// GET - how to use
// getRequest('addUrl')
//     .then(data => {
//         console.log(data);
//     });