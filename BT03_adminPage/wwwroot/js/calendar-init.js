document.addEventListener('DOMContentLoaded', function () {
    var calendarEl = document.getElementById('calendar');
    var calendar = new FullCalendar.Calendar(calendarEl, {
        initialView: 'dayGridMonth',
        locale: 'vi', // Hiển thị tiếng Việt
        headerToolbar: {
            left: 'prev,next today',
            center: 'title',
            right: 'dayGridMonth,timeGridWeek,timeGridDay'
        },
        events: [
            // Thêm các sự kiện của bạn vào đây
            {
                title: 'Tăng ca',
                start: '2025-09-20',
                color: 'red'
            },
            {
                title: 'Họp với khách hàng',
                start: '2025-09-22T10:00:00',
                end: '2025-09-22T12:00:00',
                color: 'blue'
            }
        ],
        editable: true,
        selectable: true,
        select: function (info) {
            var eventName = prompt('Nhập tên sự kiện:');
            if (eventName) {
                calendar.addEvent({
                    title: eventName,
                    start: info.startStr,
                    end: info.endStr
                });
            }
            calendar.unselect();
        }
    });
    calendar.render();
});