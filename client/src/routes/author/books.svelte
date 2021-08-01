<script context="module">
	export const prerender = true;
</script>

<script>
	import { onMount } from 'svelte';
	import { variables } from '../../variables';
	let url = variables.backendAuhtorBookUrl;
	let authorId = 0;

	async function fetchBooks() {
		console.log(url, authorId);
		const response = await fetch(url + '?id=' + authorId);
		const json = await response.json();
		return json;
	}
	async function fetchAuthor() {
		const response = await fetch(variables.backendAuhtorUrl + '/%E2%80%8B' + authorId);
		const json = await response.json();
		console.log(json);
		return json
	}

	async function removeBook(e) {
		const bookId = e.target.parentNode.parentNode.id;
		const deleteUrl = url + '?bookId=' + bookId + '&authorId=' + authorId;
		const response = await fetch(deleteUrl, {
			method: 'DELETE', // *GET, POST, PUT, DELETE, etc.
			headers: {
				'Content-Type': 'application/json',
				accept: 'text/plain'
			}
		});
		console.log(response);
		const result = await response.json();
		if (result.isSuccess) {
			listPromise = fetchBooks();
		} else {
			alert(result.errorMessage);
		}
	}
	let listPromise = Promise.resolve();
	let authorPromise = Promise.resolve();
	onMount(() => {
		const urlSearchParams = new URLSearchParams(window.location.search);
		const params = Object.fromEntries(urlSearchParams.entries());
		authorId = params.authorId;
		listPromise = fetchBooks();
		authorPromise = fetchAuthor();
	});
</script>

<svelte:head>
	<title>Home</title>
</svelte:head>
{#await authorPromise}
	<h1>List Page</h1>
{:then authorInfo}
	<h1>List Page: {authorInfo.name}</h1>
{/await}
<a href="{variables.frontendAuthorBookAdd}?authorId={authorId}">Add new book</a>

<section>
	<br />
	<div class="book-line">
		<div class="book-id bold">Id</div>
		<div class="book-name bold">Name</div>
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
				<div class="book-publishedIn">{book.publishedIn}</div>
				<div class="book-lang">{book.lang}</div>
				<div class="book-process">
					<a href="{variables.frontendBookEdit}?bookId={book.id}&authorId={authorId}">edit</a> /
					<button on:click={removeBook}>remove</button>
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
