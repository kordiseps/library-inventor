<script>
	import { onMount } from 'svelte';
	let _name;
	let _author;
	let _publishedIn;
	let _lang;

	let bookId = 0;
	let url = 'http://localhost:21285/inventory';

	async function postBook() {
		var book = {
			name: _name,
			Author: _author,
			publishedIn: _publishedIn,
			lang: _lang
		};
		console.log(bookId);
		var response = await fetch(url +  '?id=' + bookId, {
			method: 'PUT', // *GET, POST, PUT, DELETE, etc.
			headers: {
				'Content-Type': 'application/json'
			},
			body: JSON.stringify(book)
		});
		var result = await response.json();

		if (result.isSuccess) {
			window.location.href = '/';
		}		
		else{
			alert(result.errorMessage)
		}
	}
	async function fetchBook() {
		const response = await fetch(url + '/%E2%80%8B' + bookId);
		console.log(response);
		const json = await response.json();
		console.log(json);
		_name = json.name;
		_author = json.author;
		_publishedIn = json.publishedIn;
		_lang = json.lang;
		bookId = json.id;
	}

	onMount(() => {
		const urlSearchParams = new URLSearchParams(window.location.search);
		const params = Object.fromEntries(urlSearchParams.entries());
		bookId = params.bookId;
		fetchBook();
	});
</script>

<h1>Edit Page</h1>
<a href="/">Back to List</a>

<section>
	<form on:submit|preventDefault={postBook}>
		<input type="text" bind:value={_name} placeholder="Name" />
		<input type="text" bind:value={_author} placeholder="Author" />
		<input type="text" bind:value={_publishedIn} placeholder="PublishedIn" />
		<input type="text" bind:value={_lang} placeholder="Language" />
		<button type="submit">Save</button>
	</form>
</section>

<style>
	section {
		margin-top: 2em;
		display: flex;
		flex-direction: column;
		justify-content: center;
		align-items: center;
		flex: 1;
	}
	form {
		width: 100%;
	}
	input {
		display: block;
		margin-bottom: 0.5em;
		width: 50%;
		box-shadow: inset 1px 1px 6px rgba(0, 0, 0, 0.1);
		outline: none;
		border: 1px solid #ff3e00;
		font-size: 1.4em;
	}
	input:focus {
		border: 1px solid transparent;
		outline: 1px solid lightblue;
	}
</style>
