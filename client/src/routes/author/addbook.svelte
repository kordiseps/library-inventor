<script>
	import { onMount } from 'svelte';
	let _authorId=0
	let _name = ''; 
	let _publishedIn = '';
	let _lang = '';
	import { variables } from "../../variables"
	let url = variables.backendBookUrl;
	async function postBook() {
		var book = {
			name: _name,
			publishedIn: _publishedIn,
			lang: _lang,
			authorId : _authorId
		};
		var response = await fetch(url, {
			method: 'POST', // *GET, POST, PUT, DELETE, etc.
			headers: {
				'Content-Type': 'application/json'
			},
			body: JSON.stringify(book) 
		});
		var result = await response.json()

		if(result.isSuccess){
			window.location.href=variables.frontendBookList
		}
		else{
			alert(result.errorMessage)
		}
	}
	onMount(() => {
		const urlSearchParams = new URLSearchParams(window.location.search);
		const params = Object.fromEntries(urlSearchParams.entries());
		_authorId = params.authorId;
	});
</script>

<h1>Add Page</h1>
<a href="{variables.frontendAuthorBooks}?authorId={_authorId}">Back to List</a>

<section>
	<form on:submit|preventDefault={postBook}>
		<input type="text" bind:value={_name} placeholder="Name"/>
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
		margin-bottom: .5em;
		width: 50%;
		box-shadow: inset 1px 1px 6px rgba(0, 0, 0, 0.1);
		outline: none;
		border: 1px solid #ff3e00;
		font-size: 1.4em;
	}
	input:focus{
		border: 1px solid transparent;
		outline: 1px solid lightblue;
	}
</style>
