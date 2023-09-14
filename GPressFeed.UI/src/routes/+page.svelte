<script lang=ts>
  import NewsFeedList from "../components/NewsFeedList.svelte"
  import Loader from "../components/Loader.svelte";
  import GetNewsFeed from "../functions/GetNewsFeed";
</script>

{#await GetNewsFeed()}
  <Loader/>
{:then newsFeed}
  <NewsFeedList newsFeed={newsFeed}/>
{:catch error}
  <p>The API is down!</p>
{/await}