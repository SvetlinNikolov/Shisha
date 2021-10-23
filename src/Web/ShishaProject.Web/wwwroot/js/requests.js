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
        redirect: 'follow',
        referrerPolicy: 'no-referrer',
        body: JSON.stringify(data)
    });

    return response.json();
}

 function postRequestRawHtml(url = '', data = {}) {
    $.ajax({
        url: url,
        method: 'POST',
        data: data,

    }).done(function (data) {
        console.log('az sym datata',  data)
        return data;
    })
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