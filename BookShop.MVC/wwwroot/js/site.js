const RenderBookShopServices = (services, container) => {
    container.empty();

    for (const service of services) {
        container.append(
            `<div class="card border-secondary mb-3" style="max-width: 18rem;">
          <div class="card-header">${service.cost}</div>
          <div class="card-body">
            <h5 class="card-title">${service.description}</h5> 
          </div>
        </div>`)
    }
}


const LoadBookShopServices = () => {
    const container = $("#services")
    const bookShopEncodedName = container.data("encodedName");

    $.ajax({
        url: `/BookShop/${bookShopEncodedName}/BookShopService`,
        type: 'get',
        success: function (data) {
            if (!data.length) {
                container.html("There are no services for this car workshop")
            } else {
                RenderBookShopServices(data, container)
            }
        },
        error: function () {
            toastr["error"]("Something went wrong")
        }
    })
}