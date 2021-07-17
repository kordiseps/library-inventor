<script>
	let _name = 'anılar';
	let _author = 'seval';
	let _publishedIn = '2021';
	let _lang = 'tr';

	let url = 'http://localhost:21285/inventory';
	async function postBook() {
		var book = {
			name: _name,
			Author: _author,
			publishedIn: _publishedIn,
			lang: _lang
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
			window.location.href="/"
		}
	}
</script>

<h1>Add Page</h1>
<a href="/">Back to List</a>

<section>
	<form on:submit|preventDefault={postBook}>
		<input type="text" bind:value={_name} placeholder="Name"/>
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
