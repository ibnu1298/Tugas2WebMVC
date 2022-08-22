document.addEventListener('DOMContentLoaded', () => {
    const selectDrop = document.querySelector('#Role');

    fetch('https://localhost:7062/api/Admin').then(res => {
        return res.json();
    }).then(data => {
        let output = "";
        data.forEach(admin => {
            output += `<option value="${admin.name}">${admin.name}</option>`;
        })
        selectDrop.innerHTML = output;
    }).catch(err => {
        console.log(err);

    })
    
})
document.addEventListener('DOMContentLoaded', () => {
    const selectDrop = document.querySelector('#User');

    fetch('https://localhost:7062/api/User').then(res => {
        return res.json();
    }).then(data => {
        let output = "";
        data.forEach(user => {
            output += `<option value="${user.email}">${user.firstName} ${user.lastName}</option>`;
        })
        selectDrop.innerHTML = output;
    }).catch(err => {
        console.log(err);
    })
    
})
document.addEventListener('DOMContentLoaded', () => {
    const selectDrop = document.querySelector('#studentEnroll');

    fetch('https://localhost:7062/api/Student/OName').then(res => {
        return res.json();
    }).then(data => {
        let output = "";
        data.forEach(student => {
            output += `<option value="${student.id}">${student.firstMidName} ${student.lastName}</option>`;
        })
        selectDrop.innerHTML = output;
    }).catch(err => {
        console.log(err);
    })
    
})
document.addEventListener('DOMContentLoaded', () => {
    const selectDrop = document.querySelector('#courseEnroll');

    fetch('https://localhost:7062/api/Course/form').then(res => {
        return res.json();
    }).then(data => {
        let output = "";
        data.forEach(course => {
            output += `<option value="${course.courseID}">${course.title}</option>`;
        })
        selectDrop.innerHTML = output;
    }).catch(err => {
        console.log(err);
    })
    
})