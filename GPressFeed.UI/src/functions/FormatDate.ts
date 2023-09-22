export default function FormatDate(unformattedDate: Date) {
  unformattedDate = new Date(unformattedDate);

  let month =
    unformattedDate.getMonth() > 10
      ? unformattedDate.getMonth()
      : "0" + unformattedDate.getMonth();

  let formattedDate =
    unformattedDate.getDate() +
    " " +
    month +
    " " +
    unformattedDate.getFullYear();

  return formattedDate;
}
