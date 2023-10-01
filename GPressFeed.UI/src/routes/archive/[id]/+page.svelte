<script lang=ts>
  import Loader from '../../../components/Loader.svelte';
  import GetNewsFeedById from '../../../functions/GetNewsFeedById';
  import NewsFeedList from '../../../components/NewsFeedList.svelte';
  import FormatDate from '../../../functions/FormatDate';
  import ApiDownMessage from '../../../components/ApiDownMessage.svelte';

  export let data;
  const articleId: string = data.articleId;
</script>

{#await GetNewsFeedById(articleId)}
  <Loader/>
{:then newsFeed}
  <div class="column">
    <p class="articleDate">{FormatDate(newsFeed.publishDate)}</p>
    <NewsFeedList newsFeed={newsFeed}/>
  </div>
{:catch}
  <ApiDownMessage/>
{/await}