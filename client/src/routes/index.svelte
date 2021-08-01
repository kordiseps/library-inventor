<script context="module">
	export const prerender = true;
</script>

<script>
    import { variables } from "../variables"
</script>

<svelte:head>
	<title>Home</title>
</svelte:head>
<h1>Home Page</h1>

<section>
	<a href={variables.frontendBookList}>books</a>
	<br />
	<a href={variables.frontendAuthorList}>auhtors</a>
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
</style>
