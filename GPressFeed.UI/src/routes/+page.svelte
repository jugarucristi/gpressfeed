<script lang=ts>
  import NewsFeed from "../components/NewsFeed.svelte"
  import Loader from "../components/Loader.svelte";
  import GetNewsFeed from "../functions/GetNewsFeed";
</script>

{#await GetNewsFeed()}
  <Loader/>
{:then newsFeed}
  <NewsFeed newsFeed={newsFeed}/>
{:catch error}
  <p>The API is down!</p>
{/await}