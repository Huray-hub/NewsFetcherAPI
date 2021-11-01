$(() => {
    $(".category-item").each((index, item) => {
        let $item = $(item);
        $item.click(() => fetchNews($item));
    })

    const fetchNews = ($e) => {
        let category = $e.attr('id');
        let url = $e.attr('data-url');
        let finalUrl = `${url}?category=${category}`;

        $.ajax({
            type: "GET",
            url: finalUrl,
            success: (data) => {
                $("main").empty();

                data.forEach((article) => {
                    $("main ").append(`
                    <div class="card" style="width: 22rem;">
                        <div class="card-body body-card">
                            <div>
                                <h5 class="card-title">${article.title} (${article.date})</h5>
                                <p class="card-text">-source: ${article.source}</p>
                            </div>                        
                            <a href="${article.url}" class="btn btn-primary">Visit</a>
                        </div>
                    </div>
                `);
                });
            }
        })
    };
})
