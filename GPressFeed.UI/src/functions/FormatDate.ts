export default function FormatDate(unformattedDate: Date) {
  unformattedDate = new Date(unformattedDate);

  let month =
    unformattedDate.getMonth() > 10
      ? unformattedDate.getMonth() + 1
      : "0" + (unformattedDate.getMonth() + 1);

  let formattedDate =
    unformattedDate.getDate() +
    " " +
    month +
    " " +
    unformattedDate.getFullYear();

  return formattedDate;
}
