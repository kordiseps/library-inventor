<script>
	import '../app.css';
	import { variables } from '../variables';
</script>

<header>
	<a href={variables.frontendHome}>Home</a>
	<br />
	<a href={variables.frontendBookList}>books</a>
	<br />
	<a href={variables.frontendAuthorList}>auhtors</a>
</header>
<main>
	<slot />
</main>

<footer>
	<p>
		visit <a target="blank" href="https://github.com/kordiseps/library-inventory"
			>github.com/kordiseps/library-inventory</a
		> to see source code 
	</p>
</footer>

<style>
	header {
		display: flex;
		justify-content: space-around;
		padding-top: 1em;
		padding-bottom: 1em;
		width: 50%;
		margin: auto;
		background-color: var(--tertiary-color);
	}
	main {
		flex: 1;
		display: flex;
		flex-direction: column;
		padding: 1rem;
		width: 100%;
		max-width: 1024px;
		margin: 0 auto;
		box-sizing: border-box;
	}

	footer {
		display: flex;
		flex-direction: column;
		justify-content: center;
		align-items: center;
		padding: 40px;
	}

	footer a {
		font-weight: bold;
	}

	@media (min-width: 480px) {
		footer {
			padding: 40px 0;
		}
	}
</style>
