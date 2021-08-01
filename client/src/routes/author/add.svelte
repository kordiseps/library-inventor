<script>
	import { onMount } from 'svelte';
	let _name;
	let _country;

	let authorId = 0;
	import { variables } from "../../variables"
	let url = variables.backendAuhtorUrl;

	async function postAuthor() {
		var book = {
			name: _name, 
			country: _country
		};
		var response = await fetch(url, {
			method: 'POST',
			headers: {
				'Content-Type': 'application/json'
			},
			body: JSON.stringify(book)
		});
		var result = await response.json();

		if (result.isSuccess) {
			window.location.href=variables.frontendAuthorList
		}		
		else{
			alert(result.errorMessage)
		}
	}
</script>

<h1>Add Page</h1>
<a href={variables.frontendAuthorList}>Back to List</a>

<section>
	<form on:submit|preventDefault={postAuthor}>
		<input type="text" bind:value={_name} placeholder="Name" /> 
		<input type="text" bind:value={_country} placeholder="Country" />
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
