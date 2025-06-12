let currentPage = 1, pageSize = 10, totalCount = 0;

function loadPosts(page) {
    fetch(`/api/posts?page=${page}&pageSize=${pageSize}`)
        .then(res => res.json())
        .then(response => {
            const postsDiv = document.getElementById("posts");
            postsDiv.innerHTML = "";

            const data = response.data;
            totalCount = response.totalCount;

            data.forEach(post => {
                const postDiv = document.createElement("div");
                postDiv.className = "post";

                const bodyPreview = post.body.length > 50
                    ? post.body.slice(0, 50) + '... <a href="#" onclick="alert(`' + post.body + '`)">Read More</a>'
                    : post.body;

                postDiv.innerHTML = `
                    <h2>${post.title}</h2>
                    <p>${bodyPreview}</p>
                `;

                postsDiv.appendChild(postDiv);
            });

            // Pagination
            const totalPages = Math.ceil(totalCount / pageSize);
            const pagination = document.getElementById("pagination");
            pagination.innerHTML = `
                <button ${page === 1 ? "disabled" : ""} onclick="loadPosts(${page - 1})">Previous</button>
                <span>Page ${page} of ${totalPages}</span>
                <button ${page === totalPages ? "disabled" : ""} onclick="loadPosts(${page + 1})">Next</button>
            `;

            currentPage = page;
        });
}

window.onload = () => loadPosts(currentPage);
