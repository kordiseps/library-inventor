<script context="module">
	export const prerender = true;
</script>

<script> 
    import { variables } from "../../variables"
	let url = variables.backendAuhtorUrl

	async function fetchAuhtors() {
		const response = await fetch(url);
		const json = await response.json();
		return json;
	}

	async function removeAuthor(e) {
		const authorId = e.target.parentNode.parentNode.id;
		const deleteUrl = url + '?id=' + authorId
		const response = await fetch(deleteUrl, {
			method: 'DELETE', // *GET, POST, PUT, DELETE, etc.
			headers: {
				'Content-Type': 'application/json',
				"accept": "text/plain"
			},
		});
		const result = await response.json();
		if (result.isSuccess) {
			listPromise = fetchAuhtors();
		}		
		else{
			alert(result.errorMessage)
		}
	}
	
	let listPromise = fetchAuhtors();
</script>

<svelte:head>
	<title>Home</title>
</svelte:head>
<h1>Author List Page</h1>
<a href={variables.frontendAuthorAdd}>Add new author</a>

<section>
	<br />
	<div class="author-line">
		<div class="author-id bold">Id</div>
		<div class="author-name bold">Name</div>  
		<div class="author-country bold">Country</div>
		<div class="author-process bold">#</div>
	</div>
	{#await listPromise}
		<p>loading</p>
	{:then authors}
		{#each authors as author}
			<div class="author-line" id={author.id}>
				<div class="author-id">{author.id}</div>
				<div class="author-name">{author.name}</div>
				<div class="author-country">{author.country}</div>
				<div class="author-process">
					<a href="{variables.frontendAuthorBooks}?authorId={author.id}">books</a> /
					<a href="{variables.frontendAuthorEdit}?authorId={author.id}">edit</a> / 
					<button on:click={removeAuthor}>remove</button>
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
	.author-line {
		display: flex;
		flex-direction: row;
		width: 100%;
		margin-bottom: 1em;
	}
	.author-id {
		width: 5%;
	}
	.author-name {
		width: 30%;
	}
	.author-country {
		width: 10%;
	}
	.author-process {
		width: 55%;
	}
</style>
