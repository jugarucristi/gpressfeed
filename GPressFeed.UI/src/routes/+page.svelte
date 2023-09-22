<script lang=ts>
  import NewsFeedList from "../components/NewsFeedList.svelte"
  import Loader from "../components/Loader.svelte";
  import GetTodaysNewsFeed from "../functions/GetTodaysNewsFeed";
</script>

{#await GetTodaysNewsFeed()}
  <Loader/>
{:then newsFeed}
<div class="column">
  <p class="articleDate">Today</p>
  <NewsFeedList newsFeed={newsFeed}/>
</div>
{:catch}
  <p>The API is down!</p>
{/await}