<script context="module">
	export const prerender = true;
</script>

<script>
	let url = 'http://localhost:21285/inventory';

	async function fetchBooks() {
		const response = await fetch(url);
		const json = await response.json();
		return json;
	}

	async function removeBook(e) {
		const bookId = e.target.parentNode.parentNode.id;
		var bookIdEncoded = encodeURI(bookId);
		//const deleteUrl = url + '/' + bookIdEncoded
		const deleteUrl = url + '?id=' + bookId
		const response = await fetch(deleteUrl, {
			method: 'DELETE', // *GET, POST, PUT, DELETE, etc.
			headers: {
				'Content-Type': 'application/json',
				"accept": "text/plain"
			},
		});
		console.log(response);
		const result = await response.json();
		if (result.isSuccess) {
			listPromise = fetchBooks();
		}
	}
	
	let listPromise = fetchBooks();
</script>

<svelte:head>
	<title>Home</title>
</svelte:head>
<h1>List Page</h1>
<a href="/add">Add new book</a>

<section>
	<br />
	<div class="book-line">
		<div class="book-id bold">Id</div>
		<div class="book-name bold">Name</div>
		<div class="book-author bold">Author</div>
		<div class="book-publishedIn bold">Published In</div>
		<div class="book-lang bold">Lang</div>
		<div class="book-process bold">#</div>
	</div>
	{#await listPromise}
		<p>loading</p>
	{:then books}
		{#each books as book}
			<div class="book-line" id={book.id}>
				<div class="book-id">{book.id}</div>
				<div class="book-name">{book.name}</div>
				<div class="book-author">{book.author}</div>
				<div class="book-publishedIn">{book.publishedIn}</div>
				<div class="book-lang">{book.lang}</div>
				<div class="book-process">
					<a href="/edit?bookId={book.id}">edit</a> / <button on:click={removeBook}>remove</button>
				</div>
			</div>
		{/each}
	{/await}
</section>

<style>
	div {
		font-size: 18px;
	}
	section {
		margin-top: 2em;
		display: flex;
		flex-direction: column;
		justify-content: center;
		align-items: center;
		flex: 1;
	}
	.bold {
		font-weight: 600;
	}
	.book-line {
		display: flex;
		flex-direction: row;
		width: 100%;
		margin-bottom: 1em;
	}
	.book-id {
		width: 5%;
	}
	.book-name {
		width: 30%;
	}
	.book-author {
		width: 25%;
	}
	.book-publishedIn {
		width: 15%;
	}
	.book-lang {
		width: 10%;
	}
	.book-process {
		width: 15%;
	}
</style>
