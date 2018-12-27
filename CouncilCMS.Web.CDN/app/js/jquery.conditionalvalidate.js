$(['requiredif']).each(function (index, validationName) {
    $.validator.addMethod(validationName,
            function (value, element, parameters) {
                var prefix = "";
                var lastDot = element.name.lastIndexOf('.');

                if (lastDot != -1)
                    prefix = element.name.substring(0, lastDot + 1).replace('.', '_');
                
                var id = '#' + prefix + parameters['dependentproperty'];
                var targetValue = parameters['targetvalue'];

                targetValue = (targetValue == null ? '' : targetValue).toString();
                
                var control = $(id);

                if (control.length == 0 && prefix.length > 0)
                    control = $('#' + parameters['dependentproperty']);
                
                if (control.length > 0) {
                    var controlType = control.attr('type');
                    var actualValue = "";

                    switch (controlType) {
                        case 'checkbox':
                            actualValue = control.is(':checked').toString(); break;
                        case 'select':
                            actualValue = $('option:selected', control).text(); break;
                        default:
                            actualValue = control.val(); break;
                    }
                    
                    if (targetValue.toLowerCase() === actualValue.toLowerCase()) {
                        var rule = parameters['rule'];
                        var ruleparam = parameters['ruleparam'];
                        return $.validator.methods[rule].call(this, value, element, ruleparam);
                    }
                }

                return true;
            }
        );

    $.validator.unobtrusive.adapters.add(validationName, ['dependentproperty', 'targetvalue', 'rule', 'ruleparam'], function (options) {
        var ruleParam = options.params['ruleparam'];

        options.rules[validationName] = {
            dependentproperty: options.params['dependentproperty'],
            targetvalue: options.params['targetvalue'],
            rule: options.params['rule']
        };

        if (ruleParam) {
            options.rules[validationName].ruleparam = ruleParam.charAt(0) == '[' ? eval(ruleParam) : ruleParam;
        }

        options.messages[validationName] = options.message;
    });
});