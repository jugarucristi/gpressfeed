<script lang="ts">
  import ArchivedFeedList from "../../components/ArchivedFeedList.svelte";
  import Loader from "../../components/Loader.svelte";
  import GetNewsFeedHistory from "../../functions/GetNewsFeedHistory";
</script>

{#await GetNewsFeedHistory(10)}
  <Loader/>
{:then archivedFeeds}
  <ArchivedFeedList archivedFeeds={archivedFeeds}/>
{:catch}
  <p>The API is down!</p>
{/await}