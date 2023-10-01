export default function FormatDate(unformattedDate: Date) {
  unformattedDate = new Date(unformattedDate);
  let day =
    unformattedDate.getDate() > 9
      ? unformattedDate.getDate()
      : "0" + unformattedDate.getDate();

  let month =
    unformattedDate.getMonth() + 1 > 9
      ? unformattedDate.getMonth() + 1
      : "0" + (unformattedDate.getMonth() + 1);

  let formattedDate = day + " " + month + " " + unformattedDate.getFullYear();

  return formattedDate;
}
