let currentPage = 1, pageSize = 10, totalCount = 0;

function loadPosts(page) {
    fetch(`/api/home?page=${page}&pageSize=${pageSize}`)
        .then(res => {
            if (!res.ok) {
                throw new Error(`HTTP error! Status: ${res.status}`);
            }
            return res.json();
        })
        .then(response => {
            if (!response.data || !Array.isArray(response.data)) {
                throw new Error("Invalid response format");
            }

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
        })
        .catch(error => {
            console.error("Failed to load posts:", error);
            const postsDiv = document.getElementById("posts");
            postsDiv.innerHTML = `<p style="color:red;">Error loading posts. Please try again later.</p>`;
        });
}

window.onload = () => loadPosts(currentPage);
