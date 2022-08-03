
let count = 0;
let maxCount;
let pageSize = 4;// я поставил максимально 5 записей с сервера, но если нужно можно сделать больше
window.addEventListener('load',GetCurrency(count));
function GetCurrency(count)
{
 $.ajax(
	{   
		url:'https://localhost:44375/сurrencies?pageSize='+pageSize+'&pageNumber=' + count,
		success: (result) => {
			let data = JSON.parse(JSON.stringify(result));
            maxCount = data.length;
			$('#Currency-name').html(data);
			$('#Currency-value').html(data);
			for(dat of data)
			{
              $('#Currency-name').append('<div class="curency">' + dat.name + '<div/>'+ '<div class="currency-value">' + dat.value + 'руб. <div/>');
             // $('#Currency-value').append();
			}
		}

	})
} 

$('#nextBt').click(()=>
{
	if(maxCount < pageSize && maxCount != 0)
	{
	  document.querySelector('#nextBt').style.display = 'none';	
	} else
	{
		document.querySelector('#prevBt').style.display = 'block';
      count++;
	}
	GetCurrency(count);
})
$('#prevBt').click(()=>
{
	
	if(count > 0)
	{
	document.querySelector('#nextBt').style.display = 'block';
	count--;
	GetCurrency(count);
      }
    else 
    {
    	document.querySelector('#prevBt').style.display = 'none';
    }
})
$('#findBt').click(()=>
{
	const currencyId = document.querySelector('#findTb').value;
	if(currencyId){
	$.ajax(
	{   
		url:'https://localhost:44375/сurrency/' + currencyId,
		success: (result) => {
			let data1 = JSON.parse(JSON.stringify(result));
			$('#currency-from-id-name').html(data1);
              $('#currency-from-id-name').append('<div class="curency">' + data1.name + '<div/>'+ '<div class="currency-value">' + data1.value + 'руб. <div/>');
			
		},
		statusCode: {
                400: function() {
      alert( "Запись не найдена" );
                     }
                 }
	})

   } else 
   {
   	alert("Введите значение")
   }
})