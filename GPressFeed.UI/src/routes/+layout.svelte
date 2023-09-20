<script lang="ts">
	import PageHeader from "../components/PageHeader.svelte";
</script>

<style>
	@import 'layout.css';
</style>

<PageHeader/>
<div class="pagecontent">
    <slot/>
</div>
<div class="footer">Cristi Jugaru &#x2022 {new Date().getFullYear()}</div>